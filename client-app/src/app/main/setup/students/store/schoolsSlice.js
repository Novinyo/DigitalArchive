import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getSchools = createAsyncThunk(
  'setupApp/schools/getSchools',
  async (params, { getState }) => {
    const response = await axios.get('/api/schools/activeschools');

    const data = await response.data;

    const schools = data.map((x) => {
      return { id: x.id, name: x.name };
    });
    schools.unshift({ id: '', name: 'Please select...' });

    return schools;
  }
);

const schoolsAdapter = createEntityAdapter({});

export const { selectAll: selectSchools, selectById: selectSchoolsById } =
  schoolsAdapter.getSelectors((state) => state.contactsApp.schools);

const schoolsSlice = createSlice({
  name: 'setupApp/schools',
  initialState: schoolsAdapter.getInitialState([]),
  reducers: {},
  extraReducers: {
    [getSchools.fulfilled]: schoolsAdapter.setAll,
  },
});

export default schoolsSlice.reducer;
