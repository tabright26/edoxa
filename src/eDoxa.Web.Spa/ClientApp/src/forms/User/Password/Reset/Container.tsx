import { connect } from "react-redux";
import Reset from "./Reset";
import { resetUserPassword } from "store/root/user/password/actions";
import { RESET_USER_PASSWORD_FAIL, UserPasswordActions } from "store/root/user/password/types";
import { throwSubmissionError } from "utils/form/types";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    resetUserPassword: (data: any) => {
      delete data.confirmPassword;
      data.code = ownProps.code;
      return dispatch(resetUserPassword(data)).then((action: UserPasswordActions) => {
        switch (action.type) {
          case RESET_USER_PASSWORD_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
          default: {
            window.location.href = `${process.env.REACT_APP_AUTHORITY}/account/login`;
            break;
          }
        }
      });
    }
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Reset);
