import TextField from '@mui/material/TextField';
import InputAdornment from '@mui/material/InputAdornment';
import FuseSvgIcon from '@fuse/core/FuseSvgIcon';
import FuseLoading from '@fuse/core/FuseLoading';
import { useSelector } from 'react-redux';
import { Controller, useFormContext } from 'react-hook-form';
import Switch from '@mui/material/Switch';
import { useState } from 'react';
import Autocomplete from '@mui/material/Autocomplete/Autocomplete';
import Checkbox from '@mui/material/Checkbox/Checkbox';
import _ from '@lodash';
import { selectRoles } from '../../store/rolesSlice';

function AdditionalTab(props) {
  const methods = useFormContext();
  const { control, formState } = methods;
  const { errors } = formState;
  const roles = useSelector(selectRoles);
  const [canView, setCanView] = useState(false);

  const handleSwitch = (evt) => {
    setCanView(evt.target.checked);
  };
  if (roles.length < 1 || !roles) {
    return <FuseLoading />;
  }

  return (
    <div>
      <Controller
        control={control}
        name="roles"
        render={({ field: { onChange, value } }) => (
          <Autocomplete
            multiple
            id="roles"
            className="mt-32"
            options={roles}
            disableCloseOnSelect
            getOptionLabel={(option) => {
              return option.name;
            }}
            renderOption={(_props, option, { selected }) => (
              <li {..._props}>
                <Checkbox style={{ marginRight: 8 }} checked={selected} />
                {option.name}
              </li>
            )}
            value={value ? value.map((id) => _.find(roles, { id })) : []}
            onChange={(event, newValue) => {
              onChange(newValue.map((item) => item.id));
            }}
            fullWidth
            renderInput={(params) => <TextField {...params} label="Roles" placeholder="Roles" />}
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
          <Switch
            {...field}
            onClick={(evt) => {
              handleSwitch(evt);
            }}
            className="mt-32"
            id="hasMedicalRecord"
            variant="outlined"
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

export default AdditionalTab;
