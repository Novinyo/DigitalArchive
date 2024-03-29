import FuseUtils from '@fuse/utils/FuseUtils';
import axios from 'axios';
import jwtDecode from 'jwt-decode';
import jwtServiceConfig from './jwtServiceConfig';

/* eslint-disable camelcase */

class JwtService extends FuseUtils.EventEmitter {
  init() {
    this.setInterceptors();
    this.handleAuthentication();
  }

  setInterceptors = () => {
    axios.interceptors.response.use(
      (response) => {
        return response;
      },
      (err) => {
        return new Promise((resolve, reject) => {
          if (err.response.status === 401 && err.config && !err.config.__isRetryRequest) {
            // if you ever get an unauthorized response, logout the user
            this.emit('onAutoLogout', 'Invalid access_token');
            this.setSession(null);
          }
          throw err;
        });
      }
    );
  };

  handleAuthentication = () => {
    const access_token = this.getAccessToken();

    if (!access_token) {
      this.emit('onNoAccessToken');

      return;
    }

    if (this.isAuthTokenValid(access_token)) {
      this.setSession(access_token);
      this.emit('onAutoLogin', true);
    } else {
      this.setSession(null);
      this.emit('onAutoLogout', 'access_token expired');
    }
  };

  createUser = (data) => {
    const userData = {
      email: data.email.trim(),
      password: data.password,
      matchPassword: data.passwordConfirm,
    };

    return new Promise((resolve, reject) => {
      axios.post(jwtServiceConfig.signUp, userData).then((response) => {
        if (response.data.isSuccess) {
          const { school } = response.data.value.user.data;
          this.setSession(response.data.value.access_Token, school);
          resolve(response.data.value.user);
          this.emit('onLogin', response.data.value.user);
        } else {
          const errors = [{ type: 'main', message: response.data.error }];
          reject(errors);
        }
      });
    });
  };

  signInWithEmailAndPassword = (email, password) => {
    const data = {
      username: email,
      password,
    };

    return new Promise((resolve, reject) => {
      axios.post(jwtServiceConfig.signIn, data).then((response) => {
        if (response.data.isSuccess) {
          const { school } = response.data.value.user.data;
          this.setSession(response.data.value.access_Token, school);
          resolve(response.data.value.user);
          this.emit('onLogin', response.data.value.user);
        } else {
          const errors = [{ type: 'main', message: response.data.error }];
          reject(errors);
        }
      });
    });
  };

  signInWithToken = () => {
    return new Promise((resolve, reject) => {
      axios
        .get(jwtServiceConfig.accessToken, {
          data: {
            access_token: this.getAccessToken(),
          },
        })
        .then((response) => {
          if (response.data.user) {
            const { school } = response.data.user.data;

            this.setSession(response.data.access_Token, school);
            resolve(response.data.user);
          } else {
            this.logout();
            reject(new Error('Failed to login with token.'));
          }
        })
        .catch((error) => {
          this.logout();
          reject(new Error('Failed to login with token.'));
        });
    });
  };

  updateUserData = (user) => {
    return axios.post(jwtServiceConfig.updateUser, {
      user,
    });
  };

  setSession = (access_token, school) => {
    if (access_token) {
      localStorage.setItem('jwt_access_token', access_token);
      localStorage.setItem('school', JSON.stringify(school));
      axios.defaults.headers.common.Authorization = `Bearer ${access_token}`;
    } else {
      localStorage.removeItem('jwt_access_token');
      localStorage.removeItem('school');
      delete axios.defaults.headers.common.Authorization;
    }
  };

  logout = () => {
    localStorage.removeItem('jwt_access_token');
    localStorage.removeItem('school');
    this.setSession(null);
    this.emit('onLogout', 'Logged out');
  };

  isAuthTokenValid = (access_token) => {
    if (!access_token) {
      return false;
    }
    const decoded = jwtDecode(access_token);
    const currentTime = Date.now() / 1000;
    if (decoded.exp < currentTime) {
      console.warn('access token expired');
      return false;
    }

    return true;
  };

  getAccessToken = () => {
    return window.localStorage.getItem('jwt_access_token');
  };

  getSchoolInfo = () => {
    return window.localStorage.getItem('school');
  };
}

const instance = new JwtService();

export default instance;
