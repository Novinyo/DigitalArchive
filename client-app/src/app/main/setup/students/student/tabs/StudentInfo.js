import TextField from '@mui/material/TextField';
import RadioGroup from '@mui/material/RadioGroup';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import InputAdornment from '@mui/material/InputAdornment';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import { Controller, useFormContext } from 'react-hook-form';
import { FormControlLabel, FormLabel, Radio } from '@mui/material';

function StudentInfo(props) {
  const methods = useFormContext();
  const { control, formState } = methods;
  const { errors } = formState;

  return (
    <div>
      <Controller
        control={control}
        name="code"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Student Code"
            placeholder="Student Code"
            id="code"
            error={!!errors.code}
            helperText={errors?.code?.message}
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
        name="firstName"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="First Name"
            placeholder="First Name"
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
        name="middleName"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Middle Name"
            placeholder="Middle Name"
            id="middleName"
            variant="outlined"
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
        name="lastName"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Last Name"
            placeholder="Last Name"
            id="lastName"
            error={!!errors.lastName}
            helperText={errors?.lastName?.message}
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
      <>
        <FormLabel component="legend" className="mt-32">
          Gender
        </FormLabel>
        <Controller
          control={control}
          name="gender"
          render={({ field }) => (
            <RadioGroup {...field} row className="">
              <FormControlLabel value="0" control={<Radio />} label="Male" />
              <FormControlLabel value="1" control={<Radio />} label="Female" />
            </RadioGroup>
          )}
        />
      </>
      <Controller
        control={control}
        name="birthdate"
        render={({ field }) => (
          <DatePicker
            {...field}
            className="w-full"
            clearable
            renderInput={(_props) => (
              <TextField
                {..._props}
                className="mt-32"
                id="birthdate"
                label="Birth Date"
                type="date"
                required
                error={!!errors.birthdate}
                helperText={errors?.birthdate?.message}
                InputLabelProps={{
                  shrink: true,
                }}
                variant="outlined"
                fullWidth
              />
            )}
          />
        )}
      />
      <Controller
        control={control}
        name="dateJoined"
        render={({ field }) => (
          <DatePicker
            {...field}
            className="w-full"
            clearable
            renderInput={(_props) => (
              <TextField
                {..._props}
                className="mt-32"
                id="dateJoined"
                label="Start Date"
                type="date"
                required
                error={!!errors.hiredate}
                helperText={errors?.hiredate?.message}
                InputLabelProps={{
                  shrink: true,
                }}
                variant="outlined"
                fullWidth
              />
            )}
          />
        )}
      />
    </div>
  );
}

export default StudentInfo;
