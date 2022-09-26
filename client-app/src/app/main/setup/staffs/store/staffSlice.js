import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';
import history from '@history';
import StaffModel from '../model/StaffModel';

export const getStaff = createAsyncThunk(
  'staffsApp/task/getStaff',
  async (id, { dispatch, getState }) => {
    try {
      const response = await axios.get(`/api/staffs/${id}`);

      const data = await response.data;

      return data;
    } catch (error) {
      history.push({ pathname: `/setup/staffs` });
      return null;
    }
  }
);

export const addStaff = createAsyncThunk(
  'staffsApp/staffs/addStaff',
  async (staff, { dispstch, getState }) => {
    const response = await axios.post('/api/staffs', staff);

    const data = await response.data;

    return data;
  }
);

export const updateStaff = createAsyncThunk(
  'staffsApp/staffs/updateStaff',
  async (staff, { dispstch, getState }) => {
    const response = await axios.put(`/api/staffs/${staff.id}`, staff);

    const data = await response.data;

    return data;
  }
);

export const removeStaff = createAsyncThunk(
  'staffsApp/staffs/removeStaff',
  async (id, { dispstch, getState }) => {
    const response = await axios.delete(`/api/staffs/${id}`);

    await response.data;

    return id;
  }
);

export const selectStaff = ({ staffsApp }) => staffsApp.staff;

const staffSlice = createSlice({
  name: 'staffsApp/staff',
  initialState: null,
  reducers: {
    newStaff: (state, action) => StaffModel(),
    resetStaff: () => null,
  },
  extraReducers: {
    [getStaff.pending]: (state, action) => null,
    [addStaff.fulfilled]: (state, action) => action.payload,
    [updateStaff.fulfilled]: (state, action) => action.payload,
    [removeStaff.fulfilled]: (state, action) => null,
  },
});

export const { resetStaff, newStaff } = staffSlice.actions;

export default staffSlice.reducer;
