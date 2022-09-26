import useThemeMediaQuery from '@fuse/hooks/useThemeMediaQuery';
import FusePageCarded from '@fuse/core/FusePageCarded';
import withReducer from 'app/store/withReducer';
import StaffTypesTable from './StaffTypesTable';
import StaffTypesHeader from './StaffTypesHeader';
import reducer from '../../store';

function StaffTypes(props) {
  const isMobile = useThemeMediaQuery((theme) => theme.breakpoints.down('lg'));
  return (
    <FusePageCarded
      header={<StaffTypesHeader />}
      content={<StaffTypesTable />}
      scroll={isMobile ? 'normal' : 'content'}
    />
  );
}

export default withReducer('setupApp', reducer)(StaffTypes);
