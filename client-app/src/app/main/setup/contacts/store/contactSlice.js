import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';
import history from '@history';
import ContactModel from '../model/ContactModel';

export const getContact = createAsyncThunk(
  'setupApp/task/getContact',
  async (id, { dispatch, getState }) => {
    try {
      const response = await axios.get(`/api/staff/${id}`);

      const data = await response.data;
      return data;
    } catch (error) {
      history.push({ pathname: `/setup/contacts` });

      return null;
    }
  }
);

export const addContact = createAsyncThunk(
  'setupApp/contacts/addContact',
  async (contact, { dispatch, getState }) => {
    console.log(contact);
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

export const updateContact = createAsyncThunk(
  'setupApp/contacts/updateContact',
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
    const response = await axios.put(`/api/staff/${contact.id}`, staff);

    const data = await response.data;

    return data;
  }
);

export const removeContact = createAsyncThunk(
  'setupApp/contacts/removeContact',
  async (id, { dispatch, getState }) => {
    const response = await axios.delete(`/api/staff/${id}`);

    await response.data;

    return id;
  }
);

export const selectContact = ({ contactsApp }) => contactsApp.contact;

const contactSlice = createSlice({
  name: 'setupApp/contact',
  initialState: null,
  reducers: {
    newContact: (state, action) => ContactModel(),
    resetContact: () => null,
  },
  extraReducers: {
    [getContact.pending]: (state, action) => null,
    [getContact.fulfilled]: (state, action) => action.payload,
    [updateContact.fulfilled]: (state, action) => action.payload,
    [removeContact.fulfilled]: (state, action) => null,
  },
});

export const { resetContact, newContact } = contactSlice.actions;

export default contactSlice.reducer;
