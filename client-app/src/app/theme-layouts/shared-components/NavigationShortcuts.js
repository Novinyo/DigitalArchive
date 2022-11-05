import { useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';

function NavigationShortcuts(props) {
  const [school, setSchool] = useState({});
  useEffect(() => {
    const val = window.localStorage.getItem('school');
    setSchool(JSON.parse(val));
  }, []);

  return (
    <Typography className="text-3xl font-semibold tracking-tight leading-8">
      {school.name}
    </Typography>
  );
}

export default NavigationShortcuts;
