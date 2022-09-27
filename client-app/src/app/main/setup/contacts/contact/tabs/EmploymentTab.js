import TextField from '@mui/material/TextField';
import InputAdornment from '@mui/material/InputAdornment';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import MenuItem from '@mui/material/MenuItem';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { Controller, useFormContext } from 'react-hook-form';

function EmploymentTab(props) {
  const methods = useFormContext();
  const { control, formState } = methods;
  const { errors } = formState;
  const { staffTypes, schools } = props;

  return (
    <div>
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
        name="staffTypeId"
        control={control}
        render={({ field }) => (
          <>
            <TextField
              select
              {...(field ?? '')}
              label="Staff Type"
              id="staffTypeId"
              error={!!errors.staffTypeId}
              helperText={errors?.staffTypeId?.message}
              className="mt-32"
              fullWidth
            >
              {staffTypes.map((item) => (
                <MenuItem key={item.id} value={item.id}>
                  {item.name}
                </MenuItem>
              ))}
            </TextField>
          </>
        )}
      />
      <Controller
        control={control}
        name="email"
        render={({ field }) => (
          <TextField
            {...field}
            className="mt-32"
            label="Email"
            placeholder="Email"
            variant="outlined"
            fullWidth
            error={!!errors.email}
            helperText={errors?.email?.message}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <FuseSvgIcon size={20}>heroicons-solid:mail</FuseSvgIcon>
                </InputAdornment>
              ),
            }}
          />
        )}
      />
      <Controller
        control={control}
        name="phoneNumber"
        render={({ field }) => (
          <TextField
            {...field}
            className="mt-32"
            label="Phone Number"
            placeholder="Phone Number"
            variant="outlined"
            fullWidth
            error={!!errors.phoneNumber}
            helperText={errors?.phoneNumber?.message}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <FuseSvgIcon size={20}>heroicons-solid:phone</FuseSvgIcon>
                </InputAdornment>
              ),
            }}
          />
        )}
      />
      <Controller
        control={control}
        name="hiredate"
        render={({ field }) => (
          <DatePicker
            {...field}
            className="w-full"
            clearable
            renderInput={(_props) => (
              <TextField
                {..._props}
                className="mt-32"
                id="hiredate"
                label="Hire Date"
                type="date"
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
      <Controller
        control={control}
        name="schoolId"
        render={({ field }) => (
          <TextField
            select
            className="mt-32"
            {...(field ?? '')}
            label="School"
            id="schoolId"
            error={!!errors.schoolId}
            helperText={errors?.schoolId?.message}
            variant="outlined"
            fullWidth
          >
            {schools.map((item) => (
              <MenuItem key={item.id} value={item.id}>
                {item.name}
              </MenuItem>
            ))}
          </TextField>
        )}
      />
    </div>
  );
}

export default EmploymentTab;
