import TextField from '@mui/material/TextField';
import { Controller, useFormContext } from 'react-hook-form';
import InputLabel from '@mui/material/InputLabel';
import Checkbox from '@mui/material/Checkbox';

function SchoolTypeForm(props) {
  const methods = useFormContext();
  const { control, formState } = methods;
  const { errors } = formState;
  const { handleDuplicate } = props;

  return (
    <div>
      <Controller
        name="code"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblCode">Code *</InputLabel>
            <TextField
              {...field}
              className="mt-8 mb-16"
              error={!!errors.code}
              required
              helperText={errors?.code?.message}
              labelid="lblCode"
              autoFocus
              id="code"
              variant="outlined"
              onBlur={() => {
                handleDuplicate('code', field.value);
              }}
              fullWidth
            />
          </>
        )}
      />
      <Controller
        name="name"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblName">Name *</InputLabel>
            <TextField
              {...field}
              className="mt-8 mb-16"
              error={!!errors.name}
              required
              helperText={errors?.name?.message}
              labelid="lblName"
              id="name"
              onBlur={() => {
                handleDuplicate('name', field.value);
              }}
              variant="outlined"
              fullWidth
            />
          </>
        )}
      />

      <Controller
        name="description"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblDescription">Description </InputLabel>
            <TextField
              {...field}
              className="mt-8 mb-16"
              id="description"
              labelid="lblDescription"
              type="text"
              multiline
              rows={5}
              variant="outlined"
              fullWidth
            />
          </>
        )}
      />

      <Controller
        name="active"
        control={control}
        render={({ field }) => (
          <>
            <InputLabel id="lblActive">Active </InputLabel>
            <Checkbox
              {...field}
              checked={field.value}
              labelid="lblActive"
              id="active"
              size="small"
            />
          </>
        )}
      />
    </div>
  );
}

export default SchoolTypeForm;
