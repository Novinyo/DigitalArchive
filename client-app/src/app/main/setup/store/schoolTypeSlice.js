import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getSchoolType = createAsyncThunk('setupApp/getSchoolType', async (schoolTypeId) => {
  try {
    const response = await axios.get(`/api/SchoolType/${schoolTypeId}`);
    const data = await response.data;

    return data === undefined ? null : data;
  } catch (error) {
    return error.response;
  }
});

export const checkIfExists = createAsyncThunk('setupApp/checkTypeExists', async (col) => {
  try {
    console.log(col);

    const response = await axios.get(`/api/schooltype/exists/${col.key}/${col.value}`);
    console.log(response);
    const data = await response.data;

    return data === undefined ? false : data;
  } catch (error) {
    return error.response;
  }
});

export const saveSchoolType = createAsyncThunk(
  'setupApp/saveSchoolType',
  async (typeData, { dispatch, getState }) => {
    const { id } = getState().setupApp;

    const sctData = {
      code: typeData.code.trim().toUpperCase(),
      name: typeData.name.trim(),
      categoryId: typeData.categoryId.trim().toUpperCase(),
      description: typeData.description.trim(),
      Active: typeData.active,
    };

    try {
      const response =
        typeData.id === null
          ? await axios.post(`/api/schooltype`, sctData)
          : await axios.put(`api/schooltype/${typeData.id}`, sctData);

      if (response.data.isSuccess) {
        return response.data;
      }
      return response.data;
    } catch (error) {
      console.log(error.response);
      return error.response;
    }
  }
);

export const deleteSchoolType = createAsyncThunk(
  'setupApp/deleteSchoolType',
  async (val, { dispatch, getState }) => {
    const { id } = getState().setupApp.schoolType;
    await axios.delete(`/api/schooltype/${id}`);
    return id;
  }
);

const schoolTypeSlice = createSlice({
  name: 'setupApp/schooltype',
  initialState: null,
  reducers: {
    resetSchoolType: () => null,
    newSchoolType: {
      reducer: (state, action) => action.payload,
      prepare: (event) => ({
        payload: {
          id: null,
          code: '',
          name: '',
          description: '',
          active: true,
        },
      }),
    },
  },
  extraReducers: {
    [getSchoolType.fulfilled]: (state, action) => action.payload,
    [saveSchoolType.fulfilled]: (state, action) => action.payload,
    [checkIfExists.fulfilled]: (state, action) => action.payload,
    [deleteSchoolType.fulfilled]: (state, action) => null,
  },
});

export const { newSchoolType, resetSchoolType } = schoolTypeSlice.actions;

export const selectSchoolType = ({ setupApp }) => setupApp.schoolType;

export default schoolTypeSlice.reducer;
