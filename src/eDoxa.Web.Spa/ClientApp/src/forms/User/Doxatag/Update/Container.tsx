import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadUserDoxatagHistory, updateUserDoxatag } from "store/root/user/doxatagHistory/actions";
import Update from "./Update";

const mapStateToProps = (state: RootState) => {
  const { data, error, loading } = state.user.doxatagHistory;
  return {
    initialValues: data[0] || null
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    updateUserDoxatag: (data: any) => dispatch(updateUserDoxatag(data)).then(() => dispatch(loadUserDoxatagHistory()))
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
