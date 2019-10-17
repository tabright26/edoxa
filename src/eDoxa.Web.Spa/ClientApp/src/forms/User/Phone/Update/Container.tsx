import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadUserPhone, updateUserPhone } from "store/root/user/phone/actions";
import Update from "./Update";

const mapStateToProps = (state: RootState) => {
  const { data } = state.user.phone;
  return {
    initialValues: {
      phoneNumber: data.phoneNumber
    }
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    updateUserPhone: (data: any) => dispatch(updateUserPhone(data)).then(() => dispatch(loadUserPhone()))
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
