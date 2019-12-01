import { connect } from "react-redux";
import { forgotUserPassword } from "store/root/user/password/actions";
import Forgot from "./Forgot";

const mapDispatchToProps = (dispatch: any) => {
  return {
    forgotUserPassword: (data: any) => dispatch(forgotUserPassword(data))
  };
};

export default connect(null, mapDispatchToProps)(Forgot);
