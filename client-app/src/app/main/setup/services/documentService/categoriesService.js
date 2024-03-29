import FuseUtils from '@fuse/utils/FuseUtils';
import axios from 'axios';
import categoriesServiceConfig from './categoriesServiceConfig';

class CategoriesService extends FuseUtils.EventEmitter {
  checkIfExists = (id, type, value) => {
    if (value.trim().length > 2) {
      const url =
        id === null ? `?Type=${type}&Value=${value}` : `?Type=${type}&Value=${value}&Id=${id}`;
      return new Promise((resolve, reject) => {
        axios.get(`${categoriesServiceConfig.checkDuplicate}${url}`).then((response) => {
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

const instance = new CategoriesService();

export default instance;
