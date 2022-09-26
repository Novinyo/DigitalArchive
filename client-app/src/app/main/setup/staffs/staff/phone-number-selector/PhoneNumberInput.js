import TextField from '@mui/material/TextField';
import { Controller, useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup/dist/yup';
import * as yup from 'yup';

// import { selectCountries } from '../../store/countriesSlice';
// import CountryCodeSelector from './CountryCodeSelector';

const schema = yup.object().shape({
  phoneNumber: yup.string(),
});

const defaultValues = {
  phoneNumber: '',
};

function PhoneNumberInput(props) {
  const { value, hideRemove } = props;

  const { control, formState, handleSubmit, reset } = useForm({
    mode: 'onChange',
    defaultValues,
    resolver: yupResolver(schema),
  });

  const { isValid, dirtyFields, errors } = formState;

  function onSubmit(data) {
    props.onChange(data);
  }

  return (
    <form className="flex space-x-16 mb-16" onChange={handleSubmit(onSubmit)}>
      <Controller
        control={control}
        name="phoneNumber"
        render={({ field }) => (
          <TextField
            {...field}
            label="Phone Number"
            placeholder="Phone Number"
            variant="outlined"
            fullWidth
            error={!!errors.phoneNumber}
            helperText={errors?.phoneNumber?.message}
          />
        )}
      />
      {/* <Controller
        control={control}
        name="label"
        render={({ field }) => (
          <TextField
            {...field}
            className=""
            label="Label"
            placeholder="Label"
            variant="outlined"
            fullWidth
            error={!!errors.label}
            helperText={errors?.label?.message}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <FuseSvgIcon size={20}>heroicons-solid:tag</FuseSvgIcon>
                </InputAdornment>
              ),
            }}
          />
        )}
      />
      {!hideRemove && (
        <IconButton
          onClick={(ev) => {
            ev.stopPropagation();
            props.onRemove();
          }}
        >
          <FuseSvgIcon size={20}>heroicons-solid:trash</FuseSvgIcon>
        </IconButton>
      )} */}
    </form>
  );
}

export default PhoneNumberInput;
