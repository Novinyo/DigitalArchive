import Button from '@mui/material/Button';
import NavLinkAdapter from '@fuse/core/NavLinkAdapter';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate, useParams } from 'react-router-dom';
import { useEffect } from 'react';
import FuseLoading from '@fuse/core/FuseLoading';
import _ from '@lodash';
import * as yup from 'yup';
import { Controller, useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup/dist/yup';
import Box from '@mui/system/Box';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import Avatar from '@mui/material/Avatar';
import TextField from '@mui/material/TextField';
import InputAdornment from '@mui/material/InputAdornment';
import IconButton from '@mui/material/IconButton';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import {
  addStaff,
  getStaff,
  newStaff,
  removeStaff,
  selectStaff,
  updateStaff,
} from '../store/staffSlice';
// import StaffEmailSelector from './email-selector/StaffEmailSelector';
// import PhoneNumberSelector from './phone-number-selector/PhoneNumberSelector';

/**
 * Form Validation Schema
 */
const schema = yup.object().shape({
  firstName: yup.string().required('You must enter your first name'),
});

const StaffForm = (props) => {
  const staff = useSelector(selectStaff);
  const routeParams = useParams();
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const { control, watch, reset, handleSubmit, formState, getValues } = useForm({
    mode: 'onChange',
    resolver: yupResolver(schema),
  });

  const { isValid, dirtyFields, errors } = formState;

  const form = watch();

  useEffect(() => {
    if (routeParams.id === 'new') {
      dispatch(newStaff());
    } else {
      dispatch(getStaff(routeParams.id));
    }
  }, [dispatch, routeParams]);

  useEffect(() => {
    reset({ ...staff });
  }, [staff, reset]);

  /**
   * Form Submit
   */
  function onSubmit(data) {
    if (routeParams.id === 'new') {
      dispatch(addStaff(data)).then(({ payload }) => {
        navigate(`/setup/staffs/${payload.id}`);
      });
    } else {
      dispatch(updateStaff(data));
    }
  }

  function handleRemoveStaff() {
    dispatch(removeStaff(staff.id)).then(() => {
      navigate('/setup/staffs');
    });
  }

  if (_.isEmpty(form) || !staff) {
    return <FuseLoading />;
  }

  return (
    <>
      <Box
        className="relative w-full h-160 sm:h-192 px-32 sm:px-48"
        sx={{
          backgroundColor: 'background.default',
        }}
      >
        {staff.background && (
          <img
            className="absolute inset-0 object-cover w-full h-full"
            src={staff.background}
            alt="user background"
          />
        )}
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
                          accept="image/*"
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
                                const reader = new FileReader();

                                reader.onload = () => {
                                  resolve(`data:${file.type};base64,${btoa(reader.result)}`);
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
                    src={value}
                    alt={staff.firstName}
                  >
                    {staff.firstName.charAt(0)}
                  </Avatar>
                </Box>
              )}
            />
          </div>
        </div>

        <Controller
          control={control}
          name="firstName"
          render={({ field }) => (
            <TextField
              className="mt-32"
              {...field}
              label="Name"
              placeholder="Name"
              id="firstName"
              error={!!errors.firstName}
              helperText={errors?.firstName?.message}
              variant="outlined"
              required
              fullWidth
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <FuseSvgIcon size={20}>heroicons-solid:user-circle</FuseSvgIcon>
                  </InputAdornment>
                ),
              }}
            />
          )}
        />

        <Controller
          control={control}
          name="title"
          render={({ field }) => (
            <TextField
              className="mt-32"
              {...field}
              label="Title"
              placeholder="Job title"
              id="title"
              error={!!errors.title}
              helperText={errors?.title?.message}
              variant="outlined"
              fullWidth
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <FuseSvgIcon size={20}>heroicons-solid:briefcase</FuseSvgIcon>
                  </InputAdornment>
                ),
              }}
            />
          )}
        />

        <Controller
          control={control}
          name="company"
          render={({ field }) => (
            <TextField
              className="mt-32"
              {...field}
              label="Company"
              placeholder="Company"
              id="company"
              error={!!errors.company}
              helperText={errors?.company?.message}
              variant="outlined"
              fullWidth
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <FuseSvgIcon size={20}>heroicons-solid:office-building</FuseSvgIcon>
                  </InputAdornment>
                ),
              }}
            />
          )}
        />
        {/* <Controller
          control={control}
          name="emails"
          render={({ field }) => <StaffEmailSelector className="mt-32" {...field} />}
        />

        <Controller
          control={control}
          name="phoneNumbers"
          render={({ field }) => <PhoneNumberSelector className="mt-32" {...field} />}
        /> */}

        <Controller
          control={control}
          name="address"
          render={({ field }) => (
            <TextField
              className="mt-32"
              {...field}
              label="Address"
              placeholder="Address"
              id="address"
              error={!!errors.address}
              helperText={errors?.address?.message}
              variant="outlined"
              fullWidth
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <FuseSvgIcon size={20}>heroicons-solid:location-marker</FuseSvgIcon>
                  </InputAdornment>
                ),
              }}
            />
          )}
        />
        <Controller
          control={control}
          name="birthday"
          render={({ field }) => (
            <DateTimePicker
              {...field}
              className="mt-8 mb-16 w-full"
              clearable
              showTodayButton
              renderInput={(_props) => (
                <TextField
                  {..._props}
                  className="mt-32"
                  id="birthday"
                  label="Birthday"
                  type="date"
                  InputLabelProps={{
                    shrink: true,
                  }}
                  variant="outlined"
                  fullWidth
                  InputProps={{
                    startAdornment: (
                      <InputAdornment position="start">
                        <FuseSvgIcon size={20}>heroicons-solid:cake</FuseSvgIcon>
                      </InputAdornment>
                    ),
                  }}
                />
              )}
            />
          )}
        />
        <Controller
          control={control}
          name="notes"
          render={({ field }) => (
            <TextField
              className="mt-32"
              {...field}
              label="Notes"
              placeholder="Notes"
              id="notes"
              error={!!errors.notes}
              helperText={errors?.notes?.message}
              variant="outlined"
              fullWidth
              multiline
              minRows={5}
              maxRows={10}
              InputProps={{
                className: 'max-h-min h-min items-start',
                startAdornment: (
                  <InputAdornment className="mt-16" position="start">
                    <FuseSvgIcon size={20}>heroicons-solid:menu-alt-2</FuseSvgIcon>
                  </InputAdornment>
                ),
              }}
            />
          )}
        />
      </div>

      <Box
        className="flex items-center mt-40 py-14 pr-16 pl-4 sm:pr-48 sm:pl-36 border-t"
        sx={{ backgroundColor: 'background.default' }}
      >
        {routeParams.id !== 'new' && (
          <Button color="error" onClick={handleRemoveStaff}>
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
    </>
  );
};

export default StaffForm;
