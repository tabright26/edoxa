import { connect } from "react-redux";
import { push } from "connected-react-router";
import { forgotUserPassword } from "store/root/user/password/actions";
import { FORGOT_USER_PASSWORD_FAIL, UserPasswordActions } from "store/root/user/password/types";
import Forgot from "./Forgot";
import { throwSubmissionError } from "utils/form/types";

const mapDispatchToProps = (dispatch: any) => {
  return {
    forgotUserPassword: (data: any) =>
      dispatch(forgotUserPassword(data)).then((action: UserPasswordActions) => {
        switch (action.type) {
          case FORGOT_USER_PASSWORD_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
          default: {
            dispatch(push("/"));
            break;
          }
        }
      })
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Forgot);
