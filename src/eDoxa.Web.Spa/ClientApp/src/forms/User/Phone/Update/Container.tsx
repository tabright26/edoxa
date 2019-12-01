import { connect } from "react-redux";
import { RootState } from "store/types";
import Update from "./Update";
import { updateUserPhone } from "store/root/user/phone/actions";

const mapStateToProps = (state: RootState) => {
  const { data } = state.root.user.phone;
  return {
    initialValues: data
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    updateUserPhone: (data: any) => dispatch(updateUserPhone(data))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(Update);
