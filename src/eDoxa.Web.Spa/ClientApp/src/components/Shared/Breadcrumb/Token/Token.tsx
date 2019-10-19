import React, { FunctionComponent, useState } from "react";
import { Badge, Popover, PopoverBody, PopoverHeader } from "reactstrap";
import Balance from "components/User/Account/Balance";
import BuyToken from "./Buy";
import { TOKEN } from "types";

const TokenBreadcrumb: FunctionComponent<any> = ({ className }) => {
  const [open, setOpen] = useState(false);
  return (
    <div className={className}>
      <Badge id="tokenPopover" color="dark" style={{ width: "100px" }}>
        <Balance currency={TOKEN} attribute="available" />
      </Badge>
      <Popover
        style={{
          width: "175px"
        }}
        placement="bottom"
        isOpen={open}
        target="tokenPopover"
        trigger="hover"
        delay={{ show: 0, hide: 250 }}
        toggle={() => setOpen(!open)}
      >
        <PopoverHeader>TOKEN</PopoverHeader>
        <PopoverBody>
          <dl className="row mb-0">
            <dt className="col-6">Available</dt>
            <dd className="col-6">
              <Balance currency={TOKEN} attribute="available" alignment="right" />
            </dd>
            <dt className="col-6">Pending</dt>
            <dd className="col-6">
              <Balance currency={TOKEN} attribute="pending" alignment="right" />
            </dd>
          </dl>
          <BuyToken currency={TOKEN} />
        </PopoverBody>
      </Popover>
    </div>
  );
};

export default TokenBreadcrumb;
