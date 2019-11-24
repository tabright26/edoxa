import { connect } from "react-redux";
import Reset from "./Reset";
import { resetUserPassword } from "store/root/user/password/actions";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    resetUserPassword: (data: any) => {
      delete data.confirmPassword;
      data.code = ownProps.code;
      return dispatch(resetUserPassword(data));
    }
  };
};

export default connect(null, mapDispatchToProps)(Reset);
