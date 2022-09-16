import useThemeMediaQuery from '@fuse/hooks/useThemeMediaQuery';
import FusePageCarded from '@fuse/core/FusePageCarded';
import withReducer from 'app/store/withReducer';
import SchoolTypesTable from './SchoolTypesTable';
import SchoolTypesHeader from './SchoolTypesHeader';
import reducer from '../../store';

function SchoolTypes() {
  const isMobile = useThemeMediaQuery((theme) => theme.breakpoints.down('lg'));
  return (
    <FusePageCarded
      header={<SchoolTypesHeader />}
      content={<SchoolTypesTable />}
      scroll={isMobile ? 'normal' : 'content'}
    />
  );
}

export default withReducer('setupApp', reducer)(SchoolTypes);
