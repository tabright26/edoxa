import React, { FunctionComponent, useEffect } from "react";
import { faPlus } from "@fortawesome/free-solid-svg-icons";
import { Card, CardHeader, CardBody } from "reactstrap";
import UserAddressModal from "components/Service/Identity/Address/Modal";
import { compose } from "recompose";
import { connect, MapStateToProps, MapDispatchToProps } from "react-redux";
import { RootState } from "store/types";
import { loadUserAddressBook } from "store/actions/identity";
import { show } from "redux-modal";
import { CREATE_USER_ADDRESS_MODAL } from "utils/modal/constants";
import Button from "components/Shared/Button";
import AddressList from "components/Service/Identity/Address/List";

type OwnProps = {};

type StateProps = {
  limit: number;
  length: number;
};

type DispatchProps = {
  loadAddressBook: () => void;
  showCreateAddressModal: () => void;
};

type InnerProps = StateProps & DispatchProps;

type OutterProps = {
  className?: string;
};

type Props = InnerProps & OutterProps;

const Panel: FunctionComponent<Props> = ({
  className,
  limit,
  length,
  loadAddressBook,
  showCreateAddressModal
}) => {
  useEffect((): void => {
    if (length === 0) {
      loadAddressBook();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">ADDRESS BOOK</strong>
        <small className="ml-2 my-auto text-muted">
          ({length}/{limit})
        </small>
        <Button.Link
          className="p-0 ml-auto my-auto"
          icon={faPlus}
          size="sm"
          uppercase
          onClick={() => showCreateAddressModal()}
          disabled={length >= limit}
        >
          ADD A NEW ADDRESS
        </Button.Link>
        <UserAddressModal.Create />
      </CardHeader>
      <CardBody>
        <AddressList />
      </CardBody>
    </Card>
  );
};

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  return {
    length: state.root.user.addressBook.data.length,
    limit: state.static.identity.addressBook.limit
  };
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any
) => {
  return {
    loadAddressBook: () => dispatch(loadUserAddressBook()),
    showCreateAddressModal: () => dispatch(show(CREATE_USER_ADDRESS_MODAL))
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps, mapDispatchToProps)
);

export default enhance(Panel);
