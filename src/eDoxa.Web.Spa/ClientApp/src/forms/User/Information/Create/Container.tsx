import { connect } from "react-redux";
import { loadUserInformations, createUserInformations } from "store/root/user/informations/actions";
import Create from "./Create";

const mapDispatchToProps = (dispatch: any) => {
  return {
    createUserInformations: (data: any) => dispatch(createUserInformations(data)).then(() => dispatch(loadUserInformations()))
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Create);
