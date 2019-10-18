import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadUserInformations, updateUserInformations } from "store/root/user/informations/actions";
import Update from "./Update";

const mapStateToProps = (state: RootState) => {
  const { data } = state.user.informations;
  return {
    initialValues: data
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    updateUserInformations: (data: any) => dispatch(updateUserInformations(data)).then(() => dispatch(loadUserInformations()))
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
