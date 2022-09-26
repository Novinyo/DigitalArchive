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
    hiredate: null,
    streetAddress: '',
    postAddress: '',
    notes: '',
    hasMedicalRecord: false,
    medicalNotes: '',
    staffTypeId: '',
    schoolId: '',
  });

export default ContactModel;
