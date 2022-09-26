import {
  createAsyncThunk,
  createEntityAdapter,
  createSelector,
  createSlice,
} from '@reduxjs/toolkit';
import axios from 'axios';
import FuseUtils from '@fuse/utils';
import { addStaff, removeStaff, updateStaff } from './staffSlice';

export const getStaffs = createAsyncThunk(
  'staffsApp/staffs/getStaffs',
  async (params, { getState }) => {
    const response = await axios.get('/api/staff/staffs');

    const data = await response.data;

    return { data };
  }
);

const staffsAdapter = createEntityAdapter({});

export const selectSearchText = ({ staffsApp }) => staffsApp.staffs.searchText;

export const { selectAll: selectStaffs, selectById: selectStaffsById } = staffsAdapter.getSelectors(
  (state) => state.staffsApp.staffs
);

export const selectFilteredStaffs = createSelector(
  [selectStaffs, selectSearchText],
  (staffs, searchText) => {
    if (searchText.length === 0) {
      return staffs;
    }
    return FuseUtils.filterArrayByString(staffs, searchText);
  }
);

export const selectGroupedFilteredStaffs = createSelector([selectFilteredStaffs], (staffs) => {
  return staffs
    .sort((a, b) => a.name.localeCompare(b.name, 'es', { sensitivity: 'base' }))
    .reduce((r, e) => {
      // get first letter of name of current element
      const group = e.name[0];
      // if there is no property in accumulator with this letter create it
      if (!r[group]) r[group] = { group, children: [e] };
      // if there is push current element to children array for that letter
      else r[group].children.push(e);
      // return accumulator
      return r;
    }, {});
});

const staffsSlice = createSlice({
  name: 'staffsApp/staffs',
  initialState: staffsAdapter.getInitialState({
    searchText: '',
  }),
  reducers: {
    setStaffsSearchText: {
      reducer: (state, action) => {
        state.searchText = action.payload;
      },
      prepare: (event) => ({ payload: event.target.value || '' }),
    },
  },
  extraReducers: {
    [updateStaff.fulfilled]: staffsAdapter.upsertOne,
    [addStaff.fulfilled]: staffsAdapter.addOne,
    [removeStaff.fulfilled]: (state, action) => staffsAdapter.removeOne(state, action.payload),
    [getStaffs.fulfilled]: (state, action) => {
      const { data, routeParams } = action.payload;
      staffsAdapter.setAll(state, data);
      state.searchText = '';
    },
  },
});

export const { setStaffsSearchText } = staffsSlice.actions;

export default staffsSlice.reducer;
