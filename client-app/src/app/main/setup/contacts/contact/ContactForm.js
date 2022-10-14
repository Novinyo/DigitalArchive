import Button from '@mui/material/Button';
import Tab from '@mui/material/Tab';
import Tabs from '@mui/material/Tabs';
import NavLinkAdapter from '@fuse/core/NavLinkAdapter';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import FuseLoading from '@fuse/core/FuseLoading';
import _ from '@lodash';
import * as yup from 'yup';
import { Controller, FormProvider, useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup/dist/yup';
import Box from '@mui/system/Box';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import Avatar from '@mui/material/Avatar';
import IconButton from '@mui/material/IconButton';
import BasicInfoTab from './tabs/BasicInfoTab';
import EmploymentTab from './tabs/EmploymentTab';
import AdditionalTab from './tabs/AdditionalTab';

import {
  addContact,
  getContact,
  newContact,
  removeContact,
  selectContact,
  updateContact,
} from '../store/contactSlice';
import { selectTags } from '../store/tagsSlice';
import { selectSchools } from '../store/schoolsSlice';

/**
 * Form Validation Schema
 */
const date = new Date();
const maxDate = new Date(date.getFullYear() - 18, date.getMonth(), date.getDate());

const schema = yup.object().shape({
  username: yup.string().required('You must enter a username'),
  firstName: yup.string().required('You must enter a first name'),
  middleName: yup.string(),
  lastName: yup.string().required('You must enter a first name'),
  email: yup.string().email('You must enter a valid email').required('You must enter a email'),
  phoneNumber: yup.string(),
  birthdate: yup
    .date()
    .max(new Date(maxDate), 'Staff must be 18 and above')
    .required('Date of birth is required'),
  dateJoined: yup
    .date()
    .max(new Date(), 'Date hired cannot be in the future')
    .required('You must select a hire date'),
  staffTypeId: yup.string().required('Please select a valid option'),
  schoolId: yup.string().required('Please select a valid school'),
  roles: yup.array().min(1, 'Select at least one role'),
});

const ContactForm = (props) => {
  const contact = useSelector(selectContact);
  const tags = useSelector(selectTags);
  const schools = useSelector(selectSchools);
  const routeParams = useParams();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [tabValue, setTabValue] = useState(0);

  const methods = useForm({
    mode: 'onChange',
    defaultValues: {},
    resolver: yupResolver(schema),
  });
  const { control, watch, reset, handleSubmit, formState, getValues } = methods;
  const { isValid, dirtyFields, errors } = formState;
  const form = watch();

  useEffect(() => {
    if (routeParams.id === 'new') {
      dispatch(newContact());
    } else {
      dispatch(getContact(routeParams.id));
    }
  }, [dispatch, routeParams]);

  useEffect(() => {
    reset({ ...contact });
  }, [contact, reset]);

  /**
   * Form Submit
   */
  function onSubmit(data) {
    if (routeParams.id === 'new') {
      dispatch(addContact(data)).then(({ payload }) => {
        navigate(`/setup/contacts/${payload.id}`);
      });
    } else {
      dispatch(updateContact(data)).then(({ payload }) => {
        navigate(`/setup/contacts/${payload.id}`);
      });
    }
  }

  function handleRemoveContact() {
    dispatch(removeContact(contact.id)).then(() => {
      navigate(`/setup/contacts`);
    });
  }
  /**
   * Tab Change
   */
  function handleTabChange(event, value) {
    setTabValue(value);
  }

  if (_.isEmpty(form) || !contact) {
    return <FuseLoading />;
  }

  return (
    <FormProvider {...methods}>
      <Box
        className="relative w-full h-160 sm:h-192 px-32 sm:px-48"
        sx={{
          backgroundColor: 'background.default',
        }}
      >
        <img
          className="absolute inset-0 object-cover w-full h-full"
          src="assets/images/cards/14-640x480.jpg"
          alt="user background"
        />
      </Box>

      <div className="relative flex flex-col flex-auto items-center px-24 sm:px-48">
        <div className="w-full">
          <div className="flex flex-auto items-end -mt-64">
            <Controller
              control={control}
              name="avatar"
              render={({ field: { onChange, value } }) => (
                <Box
                  sx={{
                    borderWidth: 4,
                    borderStyle: 'solid',
                    borderColor: 'background.paper',
                  }}
                  className="relative flex items-center justify-center w-128 h-128 rounded-full overflow-hidden"
                >
                  <div className="absolute inset-0 bg-black bg-opacity-50 z-10" />
                  <div className="absolute inset-0 flex items-center justify-center z-20">
                    <div>
                      <label htmlFor="button-avatar" className="flex p-8 cursor-pointer">
                        <input
                          accept="image/png, image/jpeg"
                          className="hidden"
                          id="button-avatar"
                          type="file"
                          onChange={async (e) => {
                            function readFileAsync() {
                              return new Promise((resolve, reject) => {
                                const file = e.target.files[0];
                                if (!file) {
                                  return;
                                }
                                if (file.type !== 'image/jpeg' && file.type !== 'image/png') {
                                  alert('Only jpeg or png files are allowed');
                                  return;
                                }
                                if (file.size > 5000000) {
                                  alert('File size cannot exceed 5MB');
                                  return;
                                }
                                const reader = new FileReader();

                                reader.onload = () => {
                                  resolve(`data:${file.type};base64,${window.btoa(reader.result)}`);
                                };

                                reader.onerror = reject;

                                reader.readAsBinaryString(file);
                              });
                            }

                            const newImage = await readFileAsync();

                            onChange(newImage);
                          }}
                        />
                        <FuseSvgIcon className="text-white">heroicons-outline:camera</FuseSvgIcon>
                      </label>
                    </div>
                    <div>
                      <IconButton
                        onClick={() => {
                          onChange('');
                        }}
                      >
                        <FuseSvgIcon className="text-white">heroicons-solid:trash</FuseSvgIcon>
                      </IconButton>
                    </div>
                  </div>
                  <Avatar
                    sx={{
                      backgroundColor: 'background.default',
                      color: 'text.secondary',
                    }}
                    className="object-cover w-full h-full text-64 font-bold"
                    src={
                      value ||
                      (contact.avatar &&
                        `assets/images/avatars/${contact.schoolCode}/${contact.avatar}`)
                    }
                    alt={contact.firstName}
                  >
                    {console.log(contact)}
                    {contact.firstName.charAt(0)}
                  </Avatar>
                </Box>
              )}
            />
          </div>
        </div>
        <Tabs
          value={tabValue}
          onChange={handleTabChange}
          indicatorColor="secondary"
          textColor="secondary"
          variant="scrollable"
          scrollButtons="auto"
          classes={{ root: 'w-full h-64 border-b-1' }}
        >
          <Tab className="h-64" label="Basic Info" />
          <Tab className="h-64" label="Employment" />
          <Tab className="h-64" label="Additional Info" />
        </Tabs>
        <div className="p-16 sm:p-24 max-w-3xl">
          <div className={tabValue !== 0 ? 'hidden' : ''}>
            <BasicInfoTab />
          </div>
          <div className={tabValue !== 1 ? 'hidden' : ''}>
            <EmploymentTab staffTypes={tags} schools={schools} />
          </div>
          <div className={tabValue !== 2 ? 'hidden' : ''}>
            <AdditionalTab />
          </div>
        </div>
      </div>

      <Box
        className="flex items-center mt-40 py-14 pr-16 pl-4 sm:pr-48 sm:pl-36 border-t"
        sx={{ backgroundColor: 'background.default' }}
      >
        {routeParams.id !== 'new' && (
          <Button color="error" onClick={handleRemoveContact}>
            Delete
          </Button>
        )}
        <Button className="ml-auto" component={NavLinkAdapter} to={-1}>
          Cancel
        </Button>
        <Button
          className="ml-8"
          variant="contained"
          color="secondary"
          disabled={_.isEmpty(dirtyFields) || !isValid}
          onClick={handleSubmit(onSubmit)}
        >
          Save
        </Button>
      </Box>
    </FormProvider>
  );
};

export default ContactForm;
