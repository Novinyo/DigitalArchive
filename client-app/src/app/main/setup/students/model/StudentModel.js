import _ from '@lodash';

const StudentModel = (data) =>
  _.defaults(data || {}, {
    studentId: '',
    code: '',
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
    gender: 0,
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
