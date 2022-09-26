import { forwardRef } from 'react';
import clsx from 'clsx';
import PhoneNumberInput from './PhoneNumberInput';

const PhoneNumberSelector = forwardRef(({ value, onChange, className }, ref) => {
  return (
    <div className={clsx('w-full', className)} ref={ref}>
      {value.map((item, index) => (
        <PhoneNumberInput
          value={item}
          key={index}
          onChange={(val) => {
            onChange(value.map((_item, _index) => (index === _index ? val : _item)));
          }}
          onRemove={() => {
            onChange(value.filter((_item, _index) => index !== _index));
          }}
        />
      ))}
    </div>
  );
});

export default PhoneNumberSelector;
