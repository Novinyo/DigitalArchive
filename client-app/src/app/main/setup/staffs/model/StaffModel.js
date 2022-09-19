import _ from '@lodash';

const StaffModel = (data) =>
  _.defaults(data || {}, {
    avatar: null,
    firstName: '',
    middleName: '',
    lastName: '',
    email: '',
    phnoeNumber: '',
    schoolId: '',
    title: '',
    birthday: null,
    postalAddress: '',
    streetAddress: '',
    conditions: '',
    notes: '',
    roles: [{ role: '' }],
    documents: [{ document: '' }],
  });

export default StaffModel;
