import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getSchools = createAsyncThunk('setupApp/getSchools', async () => {
  try {
    const response = await axios.get('/api/Schools/schools');
    return response.data;
  } catch (error) {
    return {
      error: {
        status: error.response.status,
        message: error.response.data.errors,
      },
    };
  }
});

export const removeSchools = createAsyncThunk(
  'setupApp/schools',
  async (schoolIds, { dispatch, getState }) => {
    await axios.delete('/api/schools/Schools', { data: schoolIds });

    return schoolIds;
  }
);

const schoolsAdapter = createEntityAdapter({});

export const { selectAll: selectSchools, selectById: selectSchoolById } =
  schoolsAdapter.getSelectors((state) => state.setupApp.schools);

const schoolsSlice = createSlice({
  name: 'setupApp/schools',
  initialState: schoolsAdapter.getInitialState({
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
    [getSchools.fulfilled]: schoolsAdapter.setAll,
    [getSchools.rejected]: (state, action) => action.payload,
  },
});

export const { setSchoolsSearchText } = schoolsSlice.actions;

export const selectSchoolsSearchText = ({ setupApp }) => setupApp.schools.searchText;

export default schoolsSlice.reducer;
