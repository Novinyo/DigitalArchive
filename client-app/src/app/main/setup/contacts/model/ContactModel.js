import _ from '@lodash';

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
    schoolId: '',
    roles: [],
  });

export default ContactModel;
