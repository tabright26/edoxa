import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadUserAddressBook, updateUserAddress } from "store/root/user/addressBook/actions";
import Update from "./Update";

const mapStateToProps = (state: RootState, ownProps: any) => {
  const { data, error, loading } = state.user.addressBook;
  return {
    initialValues: data.find(address => address.id === ownProps.addressId)
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: any) => {
  return {
    updateUserAddress: (data: any) => dispatch(updateUserAddress(ownProps.addressId, data)).then(() => dispatch(loadUserAddressBook()))
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
