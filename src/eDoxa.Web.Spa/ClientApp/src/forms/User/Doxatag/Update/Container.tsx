import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadUserDoxatagHistory, updateUserDoxatag } from "store/root/user/doxatagHistory/actions";
import Update from "./Update";

const mapStateToProps = (state: RootState) => {
  const { data } = state.user.doxatagHistory;
  const doxatag = data.sort((left: any, right: any) => (left.timestamp < right.timestamp ? 1 : -1))[0] || null;
  return {
    initialValues: doxatag
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
