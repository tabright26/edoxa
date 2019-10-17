import { connect } from "react-redux";
import { push } from "connected-react-router";
import { forgotUserPassword } from "store/root/user/password/actions";
import Forgot from "./Forgot";

const mapDispatchToProps = (dispatch: any) => {
  return {
    forgotUserPassword: (data: any) => dispatch(forgotUserPassword(data)).then(() => dispatch(push("/")))
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Forgot);
