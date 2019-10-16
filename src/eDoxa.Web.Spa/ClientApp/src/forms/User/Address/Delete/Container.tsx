import { connect } from "react-redux";
import { loadUserAddressBook, deleteUserAddress } from "store/root/user/addressBook/actions";
import Delete from "./Delete";

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    deleteUserAddress: () => dispatch(deleteUserAddress(ownProps.addressId)).then(() => dispatch(loadUserAddressBook()))
  };
};

export default connect(
  null,
  mapDispatchToProps
)(Delete);
