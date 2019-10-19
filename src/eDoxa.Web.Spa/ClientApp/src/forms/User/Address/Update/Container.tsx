import { connect } from "react-redux";
import { RootState } from "store/types";
import Update from "./Update";
import { AddressId } from "types";
import { loadUserAddressBook, updateUserAddress } from "store/root/user/addressBook/actions";
import { UserAddressBookActions, UPDATE_USER_ADDRESS_FAIL } from "store/root/user/addressBook/types";
import { throwSubmissionError } from "utils/form/types";

interface OwnProps {
  addressId: AddressId;
}

const mapStateToProps = (state: RootState, ownProps: OwnProps) => {
  const { data } = state.root.user.addressBook;
  return {
    initialValues: data.find(address => address.id === ownProps.addressId)
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    updateUserAddress: (data: any) =>
      dispatch(updateUserAddress(ownProps.addressId, data)).then((action: UserAddressBookActions) => {
        switch (action.type) {
          case UPDATE_USER_ADDRESS_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
          default: {
            dispatch(loadUserAddressBook());
            break;
          }
        }
      })
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
