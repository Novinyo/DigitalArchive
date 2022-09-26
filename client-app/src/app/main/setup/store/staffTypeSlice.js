import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getStaffType = createAsyncThunk('setupApp/getStaffType', async (staffTypeId) => {
  try {
    const response = await axios.get(`/api/stafftype/${staffTypeId}`);

    const data = await response.data;

    return data === undefined ? null : data;
  } catch (error) {
    return error.response;
  }
});

export const saveStaffType = createAsyncThunk(
  'setupApp/saveStaffType',
  async (data, { dispatch, getState }) => {
    const { id } = getState().setupApp;

    const toSave = {
      code: data.code.trim().toUpperCase(),
      name: data.name.trim(),
      description: data.description.trim(),
      schoolId: data.schoolId,
      active: data.active,
    };
    console.log(toSave);
    try {
      const response =
        data.id === null
          ? await axios.post(`/api/stafftype`, toSave)
          : await axios.put(`api/stafftype/${data.id}`, toSave);

      console.log(response);

      return response.data;
    } catch (error) {
      console.log(error.response);
      return error.response;
    }
  }
);

export const deleteStaffType = createAsyncThunk(
  'setupApp/deleteStaffType',
  async (val, { dispatch, getState }) => {
    const { id } = getState().setupApp.staffType;
    await axios.delete(`/api/stafftype/${id}`);
    return id;
  }
);

const staffTypeSlice = createSlice({
  name: 'setupApp/staffType',
  initialState: null,
  reducers: {
    resetStaffType: () => null,
    newStaffType: {
      reducer: (state, action) => action.payload,
      prepare: (event) => ({
        payload: {
          id: null,
          code: '',
          name: '',
          description: '',
          schoolId: '-1',
          active: true,
        },
      }),
    },
  },
  extraReducers: {
    [getStaffType.fulfilled]: (state, action) => action.payload,
    [saveStaffType.fulfilled]: (state, action) => action.payload,
    [deleteStaffType.fulfilled]: (state, action) => null,
  },
});

export const { newStaffType, resetStaffType } = staffTypeSlice.actions;

export const selectStaffType = ({ setupApp }) => setupApp.staffType;

export default staffTypeSlice.reducer;
