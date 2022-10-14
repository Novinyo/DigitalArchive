import TextField from '@mui/material/TextField';
import InputAdornment from '@mui/material/InputAdornment';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import { Controller, useFormContext } from 'react-hook-form';
import FormControlLabel from '@mui/material/FormControlLabel';
import Switch from '@mui/material/Switch';
import { useState } from 'react';

function AddressInfo(props) {
  const methods = useFormContext();
  const { control, formState } = methods;
  const { isValid, dirtyFields, errors } = formState;

  const [canView, setCanView] = useState(false);

  const handleSwitch = (evt) => {
    setCanView(evt.target.checked);
  };

  return (
    <div>
      <Controller
        control={control}
        name="emergencyContact"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Emergency Contact"
            placeholder="Emergency contact"
            id="emergencyContact"
            error={!!errors.emergencyContact}
            helperText={errors?.emergencyContact?.message}
            variant="outlined"
            required
            fullWidth
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
        name="streetAddress"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Street Address"
            placeholder="Street Address"
            id="streetAddress"
            error={!!errors.streetAddress}
            helperText={errors?.streetAddress?.message}
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
        name="postalAddress"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Postal Address"
            placeholder="Postal Address"
            id="postalAddress"
            variant="outlined"
            fullWidth
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <FuseSvgIcon size={20}>heroicons-solid:mail-open</FuseSvgIcon>
                </InputAdornment>
              ),
            }}
          />
        )}
      />
      <Controller
        control={control}
        name="description"
        render={({ field }) => (
          <TextField
            className="mt-32"
            {...field}
            label="Description"
            placeholder="Description"
            id="description"
            error={!!errors.description}
            helperText={errors?.description?.message}
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
      <Controller
        control={control}
        name="hasMedicalRecord"
        render={({ field }) => (
          <FormControlLabel
            control={
              <Switch
                {...field}
                onClick={(evt) => {
                  handleSwitch(evt);
                }}
                id="hasMedicalRecord"
                variant="outlined"
              />
            }
            className="mt-32"
            label="Has medical record?"
          />
        )}
      />
      {canView && (
        <Controller
          control={control}
          name="medicalNotes"
          render={({ field }) => (
            <TextField
              className="mt-24"
              {...field}
              label="Medical Conditions"
              placeholder="Medical Conditions"
              id="medicalNotes"
              error={!!errors.medicalNotes}
              helperText={errors?.medicalNotes?.message}
              variant="outlined"
              fullWidth
              multiline
              minRows={5}
              maxRows={10}
              InputProps={{
                className: 'max-h-min h-min items-start',
                startAdornment: (
                  <InputAdornment className="mt-16" position="start">
                    <FuseSvgIcon size={20}>heroicons-solid:view-grid-add</FuseSvgIcon>
                  </InputAdornment>
                ),
              }}
            />
          )}
        />
      )}
    </div>
  );
}

export default AddressInfo;
