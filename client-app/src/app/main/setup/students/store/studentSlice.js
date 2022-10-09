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
      return data;
    } catch (error) {
      history.push({ pathname: `/setup/students` });

      return null;
    }
  }
);

export const addStudent = createAsyncThunk(
  'setupApp/students/addStudent',
  async (contact, { dispatch, getState }) => {
    const staff = {
      title: contact.title,
      dob: contact.birthdate,
      dateJoined: contact.dateJoined,
      staffTypeId: contact.staffTypeId,
      schoolId: contact.schoolId,
      user: {
        avatar: contact.avatar,
        username: contact.username,
        firstName: contact.firstName,
        middleName: contact.middleName,
        lastName: contact.lastName,
        email: contact.email,
        phoneNumber: contact.phoneNumber,
      },
      roles: contact.roles,
      postalAddress: contact.postalAddress,
      streetAddress: contact.streetAddress,
      haveMedicalCondition: contact.hasMedicalRecord,
      conditionRemarks: contact.haveMedicalCondition ? contact.medicalNotes : '',
      description: contact.description,
    };
    const response = await axios.post('/api/Staff', staff);

    const data = await response.data;

    return data;
  }
);

export const updateStudent = createAsyncThunk(
  'setupApp/students/updateStudent',
  async (contact, { dispatch, getState }) => {
    const staff = {
      title: contact.title,
      dob: contact.birthdate,
      dateJoined: contact.dateJoined,
      staffTypeId: contact.staffTypeId,
      schoolId: contact.schoolId,
      user: {
        avatar: contact.avatar,
        username: contact.username,
        userId: contact.userId,
        firstName: contact.firstName,
        middleName: contact.middleName,
        lastName: contact.lastName,
        email: contact.email,
        phoneNumber: contact.phoneNumber,
      },
      roles: contact.roles,
      postalAddress: contact.postalAddress,
      streetAddress: contact.streetAddress,
      haveMedicalCondition: contact.hasMedicalRecord,
      conditionRemarks: contact.hasMedicalRecord ? contact.medicalNotes : '',
      description: contact.description,
    };
    console.log(staff);
    const response = await axios.put(`/api/students/${contact.id}`, staff);

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

export const selectStudent = ({ studentsApp }) => studentsApp.contact;

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
