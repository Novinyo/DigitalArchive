import FuseUtils from '@fuse/utils/FuseUtils';
import axios from 'axios';
import setupServiceConfig from './setupServiceConfig';

class SetupService extends FuseUtils.EventEmitter {
  checkIfExists = (id, type, value) => {
    if (value.trim().length > 2) {
      const url =
        id === null ? `?Type=${type}&Value=${value}` : `?Type=${type}&Value=${value}&Id=${id}`;
      return new Promise((resolve, reject) => {
        axios.get(`${setupServiceConfig.checkDuplicate}${url}`).then((response) => {
          if (response.status === 200) {
            resolve(response);
          } else {
            const errors = [{ type: 'main', message: response.data.error }];
            reject(errors);
          }
        });
      });
    }
    return false;
  };

  checkSchool = (id, type, value) => {
    if (value.trim().length > 2) {
      const url =
        id === null ? `?Type=${type}&Value=${value}` : `?Type=${type}&Value=${value}&Id=${id}`;
      return new Promise((resolve, reject) => {
        axios.get(`${setupServiceConfig.checkSchool}${url}`).then((response) => {
          if (response.status === 200) {
            resolve(response);
          } else {
            const errors = [{ type: 'main', message: response.data.error }];
            reject(errors);
          }
        });
      });
    }
    return false;
  };

  checkStaffType = (id, type, value, schoolId) => {
    if (value.trim().length > 2) {
      const url =
        id === null ? `?Type=${type}&Value=${value}` : `?Type=${type}&Value=${value}&Id=${id}`;
      return new Promise((resolve, reject) => {
        axios.get(`${setupServiceConfig.checkStaffType}${url}`).then((response) => {
          if (response.status === 200) {
            resolve(response);
          } else {
            const errors = [{ type: 'main', message: response.data.error }];
            reject(errors);
          }
        });
      });
    }
    return false;
  };
}

const instance = new SetupService();

export default instance;
