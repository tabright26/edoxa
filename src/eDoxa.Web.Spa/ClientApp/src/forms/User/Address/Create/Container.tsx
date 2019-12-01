import { connect } from "react-redux";
import { createUserAddress } from "store/root/user/addressBook/actions";
import Create from "./Create";

const mapDispatchToProps = (dispatch: any) => {
  return {
    createUserAddress: (data: any) => dispatch(createUserAddress(data))
  };
};

export default connect(null, mapDispatchToProps)(Create);
