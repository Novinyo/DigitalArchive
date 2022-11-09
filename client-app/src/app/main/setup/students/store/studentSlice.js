import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';
import history from '@history';
import StudentModel from '../model/StudentModel';

export const getStudent = createAsyncThunk(
  'setupApp/students/getStudent',
  async (id, { dispatch, getState }) => {
    try {
      const response = await axios.get(`/api/students/${id}`);

      const data = await response.data;

      const student = {
        studentId: data.id,
        code: data.code,
        firstName: data.firstName,
        middleName: data.middleName,
        lastName: data.lastName,
        contactId: data.contact.id,
        faFName: data.contact.fatherFirstName,
        faLName: data.contact.fatherLastName,
        moFName: data.contact.motherFirstName,
        moLName: data.contact.motherLastName,
        faEmail: data.contact.fatherEmail,
        moEmail: data.contact.motherEmail,
        faPhone: data.contact.fatherPhoneNumber,
        moPhone: data.contact.motherPhoneNumber,
        gender: data.gender,
        birthdate: data.birthdate,
        dateJoined: data.dateJoined,
        emergencyContact: data.contact.emergencyContact,
        streetAddress: data.contact.streetAddress,
        postalAddress: data.contact.postalAddress,
        description: data.description === null ? '' : data.description,
        hasMedicalRecord: data.haveMedicalCondition,
        medicalNotes: data.conditionRemarks === null ? '' : data.conditionRemarks,
        documents: data.documents,
      };
      return student;
    } catch (error) {
      history.push({ pathname: `/setup/students` });

      return null;
    }
  }
);

export const addStudent = createAsyncThunk(
  'setupApp/students/addStudent',
  async (student, { dispatch, getState }) => {
    const documents = [];
    student.documents.forEach((elt) => {
      documents.push({
        docName: elt.docName,
        docTypeId: elt.docTypeId,
        docUrl: elt.docUrl,
        docDesc: '',
        startDate: elt.startDate,
        endDate: elt.endDate,
      });
    });
    const newStudent = {
      code: student.code,
      firstName: student.firstName,
      middleName: student.middleName,
      lastName: student.lastName,
      gender: parseInt(student.gender, 2),
      dateOfBirth: student.birthdate,
      dateJoined: student.dateJoined,
      contact: {
        fatherFirstName: student.faFName,
        fatherLastName: student.faLName,
        fatherPhoneNumber: student.faPhone,
        fatherEmail: student.faEmail,
        motherFirstName: student.moFName,
        motherLastName: student.moLName,
        motherPhoneNumber: student.moPhone,
        motherEmail: student.moEmail,
        emergencyContact: student.emergencyContact,
        streetAddress: student.streetAddress,
        postalAddress: student.postalAddress,
      },
      haveMedicalCondition: student.hasMedicalRecord,
      conditionRemarks: student.hasMedicalRecord ? student.medicalNotes : '',
      description: student.description,
      documents,
    };

    console.log(newStudent);

    const response = await axios.post('/api/students', newStudent);

    const data = await response.data;

    return data;
  }
);

export const updateStudent = createAsyncThunk(
  'setupApp/students/updateStudent',
  async (student, { dispatch, getState }) => {
    const documents = [];
    student.documents.forEach((elt) => {
      documents.push({
        docName: elt.docName,
        docTypeId: elt.docTypeId,
        docUrl: elt.docUrl,
        docDesc: '',
        startDate: elt.startDate,
        endDate: elt.endDate,
      });
    });

    const oldStudent = {
      id: student.studentId,
      code: student.code,
      firstName: student.firstName,
      middleName: student.middleName,
      lastName: student.lastName,
      gender: parseInt(student.gender, 2),
      dateOfBirth: student.birthdate,
      dateJoined: student.dateJoined,
      contact: {
        fatherFirstName: student.faFName,
        fatherLastName: student.faLName,
        fatherPhoneNumber: student.faPhone,
        fatherEmail: student.faEmail,
        motherFirstName: student.moFName,
        motherLastName: student.moLName,
        motherPhoneNumber: student.moPhone,
        motherEmail: student.moEmail,
        emergencyContact: student.emergencyContact,
        streetAddress: student.streetAddress,
        postalAddress: student.postalAddress,
        id: student.contactId,
      },
      haveMedicalCondition: student.hasMedicalRecord,
      conditionRemarks: student.hasMedicalRecord ? student.medicalNotes : '',
      description: student.description,
      documents,
    };

    console.log(oldStudent);
    const response = await axios.put(`/api/students/${student.studentId}`, oldStudent);

    const data = await response.data;

    return data;
  }
);

export const removeStudent = createAsyncThunk(
  'setupApp/students/removeStudent',
  async (id, { dispatch, getState }) => {
    const response = await axios.delete(`/api/students/${id}`);

    await response.data;

    return id;
  }
);

export const selectStudent = ({ studentsApp }) => studentsApp.student;

const studentSlice = createSlice({
  name: 'setupApp/student',
  initialState: null,
  reducers: {
    newStudent: (state, action) => StudentModel(),
    resetStudent: () => null,
  },
  extraReducers: {
    [getStudent.pending]: (state, action) => null,
    [getStudent.fulfilled]: (state, action) => action.payload,
    [updateStudent.fulfilled]: (state, action) => action.payload,
    [removeStudent.fulfilled]: (state, action) => null,
  },
});

export const { resetStudent, newStudent } = studentSlice.actions;

export default studentSlice.reducer;
