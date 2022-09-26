import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getStaffTypes = createAsyncThunk('setupApp/getStafftypes', async () => {
  try {
    const response = await axios.get('/api/stafftype/stafftypes');
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

export const removeStaffTypes = createAsyncThunk(
  'setupApp/staffTypes',
  async (staffTypeIds, { dispatch, getState }) => {
    await axios.delete('/api/stafftype/', { data: staffTypeIds });

    return staffTypeIds;
  }
);

const staffTypesAdapter = createEntityAdapter({});

export const { selectAll: selectStaffTypes, selectById: selectStaffTypeById } =
  staffTypesAdapter.getSelectors((state) => state.setupApp.staffTypes);

const staffTypesSlice = createSlice({
  name: 'setupApp/staffTypes',
  initialState: staffTypesAdapter.getInitialState({
    searchText: '',
  }),
  reducers: {
    setStaffTypesSearchText: {
      reducer: (state, action) => {
        state.searchText = action.payload;
      },
      prepare: (event) => ({ payload: event.target.value || '' }),
    },
  },
  extraReducers: {
    [getStaffTypes.fulfilled]: staffTypesAdapter.setAll,
    [getStaffTypes.rejected]: (state, action) => action.payload,
  },
});

export const { setStaffTypesSearchText } = staffTypesSlice.actions;

export const selectStaffTypesSearchText = ({ setupApp }) => setupApp.staffTypes.searchText;

export default staffTypesSlice.reducer;
