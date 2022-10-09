import _ from '@lodash';

const StudentModel = (data) =>
  _.defaults(data || {}, {
    avatar: null,
    firstName: '',
    middleName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    level: '',
    school: '',
    birthdate: null,
    dateJoined: null,
    streetAddress: '',
    postalAddress: '',
    description: '',
    hasMedicalRecord: false,
    medicalNotes: '',
    schoolId: '',
  });

export default StudentModel;
