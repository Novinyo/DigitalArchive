import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getSchool = createAsyncThunk('setupApp/getSchool', async (schoolId) => {
  try {
    const response = await axios.get(`/api/Schools/${schoolId}`);

    const data = await response.data;

    return data === undefined ? null : data;
  } catch (error) {
    return error.response;
  }
});

export const saveSchool = createAsyncThunk(
  'setupApp/saveSchool',
  async (typeData, { dispatch, getState }) => {
    const { id } = getState().setupApp;

    const scData = {
      code: typeData.code,
      name: typeData.name,
      description: typeData.description,
      Active: typeData.active,
    };
    try {
      const response = await axios.post(`/api/schools`, scData);
      return response.data;
    } catch (error) {
      console.log(error.response);
      return error.response;
    }
  }
);

export const deleteSchool = createAsyncThunk(
  'setupApp/deleteSchool',
  async (val, { dispatch, getState }) => {
    const { id } = getState().setupApp.schoolType;
    await axios.delete(`/api/schools/${id}`);
    return id;
  }
);

const schoolSlice = createSlice({
  name: 'setupApp/school',
  initialState: null,
  reducers: {
    resetSchool: () => null,
    newSchool: {
      reducer: (state, action) => action.payload,
      prepare: (event) => ({
        payload: {
          id: null,
          code: '',
          name: '',
          description: '',
          schoolTypeId: '-1',
          active: true,
        },
      }),
    },
  },
  extraReducers: {
    [getSchool.fulfilled]: (state, action) => action.payload,
    [saveSchool.fulfilled]: (state, action) => action.payload,
    [deleteSchool.fulfilled]: (state, action) => null,
  },
});

export const { newSchool, resetSchool } = schoolSlice.actions;

export const selectSchool = ({ setupApp }) => setupApp.school;

export default schoolSlice.reducer;
