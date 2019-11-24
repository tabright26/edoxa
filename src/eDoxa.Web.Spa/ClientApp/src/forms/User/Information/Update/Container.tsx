import { connect } from "react-redux";
import { RootState } from "store/types";
import Update from "./Update";
import { updateUserInformations } from "store/root/user/information/actions";

const mapStateToProps = (state: RootState) => {
  const { data } = state.root.user.information;
  return {
    initialValues: data
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    updateUserInformations: (data: any) =>
      dispatch(updateUserInformations(data))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Update);
