import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getSchoolTypes = createAsyncThunk('setupApp/getSchoolTypes', async (active) => {
  try {
    const response = active
      ? await axios.get('/api/SchoolType/activeschoolTypes')
      : await axios.get('/api/SchoolType/schoolTypes');
    return response.data;
  } catch (error) {
    console.log(error);
    // const mesage = error.response.data.errors.map(x => x)
    return {
      error: {
        status: error.response.status,
        message: error.response.data.errors,
      },
    };
  }
});

export const getActiveSchoolTypes = createAsyncThunk('setupApp/getActiveSchoolTypes', async () => {
  try {
    const response = await axios.get('/api/SchoolType/schoolTypes');
    return response.data;
  } catch (error) {
    console.log(error);
    // const mesage = error.response.data.errors.map(x => x)
    return {
      error: {
        status: error.response.status,
        message: error.response.data.errors,
      },
    };
  }
});

export const removeSchoolTypes = createAsyncThunk(
  'setupApp/schoolTypes',
  async (schoolTypeIds, { dispatch, getState }) => {
    await axios.delete('/api/schoolTypes/SchoolTypes', { data: schoolTypeIds });

    return schoolTypeIds;
  }
);

const schoolTypesAdapter = createEntityAdapter({});

export const { selectAll: selectSchoolTypes, selectById: selectSchoolTypeById } =
  schoolTypesAdapter.getSelectors((state) => state.setupApp.schoolTypes);

const schoolTypesSlice = createSlice({
  name: 'setupApp/schoolTypes',
  initialState: schoolTypesAdapter.getInitialState({
    searchText: '',
  }),
  reducers: {
    setSchoolTypesSearchText: {
      reducer: (state, action) => {
        state.searchText = action.payload;
      },
      prepare: (event) => ({ payload: event.target.value || '' }),
    },
  },
  extraReducers: {
    [getSchoolTypes.fulfilled]: schoolTypesAdapter.setAll,
    [getSchoolTypes.rejected]: (state, action) => action.payload,
  },
});

export const { setSchoolTypesSearchText } = schoolTypesSlice.actions;

export const selectSchoolTypesSearchText = ({ setupApp }) => setupApp.schoolTypes.searchText;

export default schoolTypesSlice.reducer;
