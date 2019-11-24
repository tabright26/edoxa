import { connect } from "react-redux";
import { createUserInformations } from "store/root/user/information/actions";
import Create from "./Create";

const mapDispatchToProps = (dispatch: any) => {
  return {
    createUserInformations: (data: any) =>
      dispatch(createUserInformations(data))
  };
};

export default connect(null, mapDispatchToProps)(Create);
