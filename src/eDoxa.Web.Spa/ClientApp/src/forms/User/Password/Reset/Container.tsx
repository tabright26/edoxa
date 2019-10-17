import { connect } from "react-redux";
import { resetUserPassword } from "store/root/user/password/actions";
import Reset from "./Reset";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    resetUserPassword: (data: any) => {
      delete data.confirmPassword;
      data.code = ownProps.code;
      return dispatch(resetUserPassword(data)).then(() => (window.location.href = `${process.env.REACT_APP_AUTHORITY}/account/login`));
    }
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Reset);
