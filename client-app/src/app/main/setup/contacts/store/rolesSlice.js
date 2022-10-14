import { createAsyncThunk, createEntityAdapter, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';

export const getRoles = createAsyncThunk(
  'setupApp/roles/getRoles',
  async (params, { getState }) => {
    const response = await axios.get('/api/account/loadroles');

    const data = await response.data;

    return data;
  }
);

const rolesAdapter = createEntityAdapter({});

export const { selectAll: selectRoles, selectById: selectRolesById } = rolesAdapter.getSelectors(
  (state) => state.contactsApp.roles
);

const rolesSlice = createSlice({
  name: 'setupApp/roles',
  initialState: rolesAdapter.getInitialState([]),
  reducers: {},
  extraReducers: {
    [getRoles.fulfilled]: rolesAdapter.setAll,
  },
});

export default rolesSlice.reducer;
