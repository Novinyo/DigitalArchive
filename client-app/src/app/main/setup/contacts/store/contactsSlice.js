import {
  createAsyncThunk,
  createEntityAdapter,
  createSelector,
  createSlice,
} from '@reduxjs/toolkit';
import axios from 'axios';
import FuseUtils from '@fuse/utils';
import { addContact, removeContact, updateContact } from './contactSlice';

export const getContacts = createAsyncThunk(
  'setupApp/contacts/getContacts',
  async (params, { getState }) => {
    const school = JSON.parse(localStorage.getItem('school'));

    const url =
      school.schoolId === null ? '/api/staff/staffs' : `/api/staff/staffs/${school.schoolId}`;

    const response = await axios.get(url);

    const data = await response.data;

    return { data };
  }
);

const contactsAdapter = createEntityAdapter({});

export const selectSearchText = ({ contactsApp }) => contactsApp.contacts.searchText;

export const { selectAll: selectContacts, selectById: selectContactsById } =
  contactsAdapter.getSelectors((state) => state.contactsApp.contacts);

export const selectFilteredContacts = createSelector(
  [selectContacts, selectSearchText],
  (contacts, searchText) => {
    if (searchText.length === 0) {
      return contacts;
    }
    return FuseUtils.filterArrayByString(contacts, searchText);
  }
);

export const selectGroupedFilteredContacts = createSelector(
  [selectFilteredContacts],
  (contacts) => {
    return contacts
      .sort((a, b) => a.lastName.localeCompare(b.lastName, 'es', { sensitivity: 'base' }))
      .reduce((r, e) => {
        // get first letter of name of current element
        const group = e.lastName[0];
        // if there is no property in accumulator with this letter create it
        if (!r[group]) r[group] = { group, children: [e] };
        // if there is push current element to children array for that letter
        else r[group].children.push(e);
        // return accumulator
        return r;
      }, {});
  }
);

const contactsSlice = createSlice({
  name: 'setupApp/contacts',
  initialState: contactsAdapter.getInitialState({
    searchText: '',
  }),
  reducers: {
    setContactsSearchText: {
      reducer: (state, action) => {
        state.searchText = action.payload;
      },
      prepare: (event) => ({ payload: event.target.value || '' }),
    },
  },
  extraReducers: {
    [updateContact.fulfilled]: contactsAdapter.upsertOne,
    [addContact.fulfilled]: contactsAdapter.addOne,
    [removeContact.fulfilled]: (state, action) => contactsAdapter.removeOne(state, action.payload),
    [getContacts.fulfilled]: (state, action) => {
      const { data, routeParams } = action.payload;
      contactsAdapter.setAll(state, data);
      state.searchText = '';
    },
  },
});

export const { setContactsSearchText } = contactsSlice.actions;

export default contactsSlice.reducer;
