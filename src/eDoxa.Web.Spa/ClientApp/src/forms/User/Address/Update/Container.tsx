import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadUserAddressBook, updateUserAddress } from "store/root/user/addressBook/actions";
import Update from "./Update";
import { AddressId } from "types";

interface OwnProps {
  addressId: AddressId;
}

const mapStateToProps = (state: RootState, ownProps: OwnProps) => {
  const { data } = state.user.addressBook;
  return {
    initialValues: data.find(address => address.id === ownProps.addressId)
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    updateUserAddress: (data: any) => dispatch(updateUserAddress(ownProps.addressId, data)).then(() => dispatch(loadUserAddressBook()))
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
