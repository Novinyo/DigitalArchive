import _ from '@lodash';

const val = localStorage.getItem('school');
const userSchool = val === 'undefined' ? null : JSON.parse(val);
const ContactModel = (data) =>
  _.defaults(data || {}, {
    avatar: null,
    username: '',
    firstName: '',
    middleName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    title: '',
    school: '',
    birthdate: null,
    dateJoined: null,
    streetAddress: '',
    postalAddress: '',
    description: '',
    hasMedicalRecord: false,
    medicalNotes: '',
    staffTypeId: '',
    schoolId: userSchool.schoolId === null ? '' : userSchool.schoolId,
    roles: [],
  });

export default ContactModel;
