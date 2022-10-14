import _ from '@lodash';

const StudentModel = (data) =>
  _.defaults(data || {}, {
    avatar: null,
    studentCode: '',
    firstName: '',
    middleName: '',
    lastName: '',
    faFName: '',
    faLName: '',
    moFName: '',
    moLName: '',
    faEmail: '',
    moEmail: '',
    faPhone: '',
    moPhone: '',
    birthdate: null,
    dateJoined: null,
    emergencyContact: '',
    streetAddress: '',
    postalAddress: '',
    description: '',
    hasMedicalRecord: false,
    medicalNotes: '',
    schoolId: '',
  });

export default StudentModel;
