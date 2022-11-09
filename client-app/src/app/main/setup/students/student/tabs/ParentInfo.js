import TextField from '@mui/material/TextField';
import InputAdornment from '@mui/material/InputAdornment';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import { Controller, useFormContext } from 'react-hook-form';

function ParentInfo(props) {
  const methods = useFormContext();
  const { control, formState } = methods;
  const { isValid, dirtyFields, errors } = formState;

  return (
    <div>
      <Controller
        control={control}
        name="faFName"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Father's First Name"
            placeholder="First Name"
            id="faFName"
            error={!!errors.faFName}
            helperText={errors?.faFName?.message}
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
        name="faLName"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Father's Last Name"
            placeholder="Last Name"
            id="faLName"
            error={!!errors.faLName}
            helperText={errors?.faLName?.message}
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
        name="faPhone"
        render={({ field }) => (
          <TextField
            {...field}
            className="mt-32"
            id="faPhone"
            label="Father's Phone Number"
            placeholder="Phone Number"
            variant="outlined"
            fullWidth
            required
            error={!!errors.faPhone}
            helperText={errors?.faPhone?.message}
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
        name="faEmail"
        render={({ field }) => (
          <TextField
            {...field}
            className="mt-32"
            label="Father's Email"
            placeholder="Email"
            variant="outlined"
            id="faEmail"
            fullWidth
            error={!!errors.faEmail}
            helperText={errors?.faEmail?.message}
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
        name="moFName"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Mother's First Name"
            placeholder="First Name"
            id="moFName"
            error={!!errors.moFName}
            helperText={errors?.moFName?.message}
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
        name="moLName"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Mother's Last Name"
            placeholder="Last Name"
            id="moLName"
            error={!!errors.moLName}
            helperText={errors?.moLName?.message}
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
        name="moPhone"
        render={({ field }) => (
          <TextField
            {...field}
            className="mt-32"
            id="faPhone"
            label="Mother's Phone Number"
            placeholder="Phone Number"
            variant="outlined"
            fullWidth
            required
            error={!!errors.moPhone}
            helperText={errors?.moPhone?.message}
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
        name="moEmail"
        render={({ field }) => (
          <TextField
            {...field}
            className="mt-32"
            label="Mother's Email"
            placeholder="Email"
            variant="outlined"
            id="moEmail"
            fullWidth
            error={!!errors.moEmail}
            helperText={errors?.moEmail?.message}
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
    </div>
  );
}

export default ParentInfo;
