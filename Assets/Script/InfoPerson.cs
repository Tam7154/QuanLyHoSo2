using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPerson
{
    public string name;//ten id cua quan nhan
    public Dictionary<string, object> infoPerson;//ten node + value node
    /*
     * tieu doan                chxhcnvn
     * dai doi                  dltuhp
     *                          date
     * 
     * 
     * THONG TIN CHIEN SY KE KHAI
     * I BAN THAN
     * Ho va ten
     * Ho va ten dang dung
     * Sinh ngay thang nam
     * Dan toc
     * Ton giao
     * Thanh phan gia dinh ban than
     * Nguyen quan
     * Noi dang ky ho khau thuong tru
     * Nhap ngu don vi huan luyen csm
     * ngay va noi vao doan
     * ngay vao dang chinh thuc
     * trinh do hoc van ngoai ngu tieng dan toc
     * nganh nghe chuyen mon da duoc dao tao qua truong
     * nganh nghe chuyen mon tu hoc va biet lam
     * suc khoe lai
     * benh ly
     * hinh the chieu cao can nang
     * khi chat
     * con liet sy con thuong binh con benh binh gia dinh ho ngheo
     * so thich ca nhan (ca hat, the thao)
     * truoc khi nhap ngu lam gi o dau thoi gian
     * ly do di bo doi

     * suy nghi ve moi truong quan doi
     * day la moi truong tot ren luyen con nguoi
     * day la moi ruong gian nan vat va
     * nguyen vong ban than
     * duoc di hoc tieu doi truong khau doi truong
     * duoc di hoc chuyen mon ky thuat
     * di hoc sy quan du bi
     * di hoc sy quan
     * ket nap vao dang
     * ra quan khi het nghia vu

     * dia chi di phep tranh thu
     * khi can bao tin cho ai
     * dia chi
     * so dien thoai
     * 
     * II Gia dinh
     * Ho ten cha
     * nam sinh
     * con song hay da mat
     * so dien thoai
     * nghe nghiep
     * tinh trang suc khoe
     * benh tat
     * ho ten me
     * nam sinh
     * con song hay da mat
     * so dien thoai
     * nghe nghiep
     * tinh trang suc khoe
     * benh tat
     * tinh trang hon nhan hien nay cua gia dinh
     * cha me dang song chung hay moio nguoi o mot noi

     * III Ong ba noi ngoai
     * 1.Ong noi
     * nam sinh
     * con song hay da mat
     * ngay mat
     * so dien thoai
     * nghe nghiep
     * chuc vu
     * co quan cong tac
     * dang vien
     * noi o hien nay
     * suc khoe
     * benh ly
     * 2. ba noi
     * nam sinh
     * con song hay da mat
     * ngay mat
     * so dien thoai
     * nghe nghiep
     * chuc vu
     * co quan cong tac
     * noi o hien nay
     * suc khoe
     * benh ly
     * 3. ong ngoai
     * nam sinh
     * con song hay da mat
     * ngay mat
     * so dien thoai
     * nghe nghiep
     * chuc vu
     * co quan cong tac
     * noi o hien nay
     * suc khoe
     * benh ly
     * 4. ba noi
     * nam sinh
     * con song hay da mat
     * ngay mat
     * so dien thoai
     * nghe nghiep
     * chuc vu
     * co quan cong tac
     * noi o hien nay
     * suc khoe
     * benh ly
     * IV Anh chi em ruot
     * 1. Ho va ten
     * nam sinh
     * nghe nghiep
     * so dien thoai
     * 2. Ho va ten
     * nam sinh
     * nghe nghiep
     * so dien thoai
     * 3. Ho va ten
     * nam sinh
     * nghe nghiep
     * so dien thoai
     * 4. Ho va ten
     * nam sinh
     * nghe nghiep
     * so dien thoai
     * 5. Ho va ten
     * nam sinh
     * nghe nghiep
     * so dien thoai
     * V ban be
     * 1. Ho va ten ban gai
     * nam sinh
     * que quan
     * nghe nghiep
     * so dien thoai
     * 2. Ho va ten ban trai than nhat
     * nam sinh
     * que quan
     * nghe nghiep
     * so dien thoai
     * 
     * VI Quan he xa hoi
     * Ban gai than nhat
     * nam sinh
     * dia chi
     * so dien thoai
     * Ban trai than nhat
     * nam sinh
     * dia chi
     * so dien thoai
     * trong ban be nguoi than ai la nguoi co anh huong tich cuc den cuoc song va su ngiep cua dong chi
     * can bo dia phuong ma dong chi quen biet va tin nhiem
     * ho va ten        chuc vu     sdt
     * ho va ten        chuc vu     sdt
     * ho va ten        chuc vu     sdt
     * ho va ten        chuc vu     sdt
     * ho va ten        chuc vu     sdt
     * 
     * THONG TIN DON VI BO SUNG
     * I Ban than
     * II Gia dinh
     * lam gi, o dau, thoi gian truoc khi nhap ngu
     * nghe nghiep
     * noi lam viec
     * thoi gian
     * quan he xa hoi truoc khi nhap ngu
     * quan he xa hoi truoc khi nhap ngu
     * vo con neu co
     * ho va ten        nam sinh        sdt
     * tinh trang hon nhan      tren 5 nam      duoi nam 5
     * cuoc song vo chong hanh phuc     binh thuong     thuong xuyen bat hoa
     * noi o hien nay
     * dang o rieng     o cung cha me chong     o cung cha me de
     * hoan canh kinh te        kha gia     du song     kho khan
     * suc khoe     benh ly
     * chua co con, da co con       trai        gai
     * tinh hinh suc khoe cua con
     * gia dinh ben vo
     * ho va ten cha vo     nam sinh        sdt
     * nghe nghiep      noi o hien nay
     * suc khoe
     * ho va ten me vo      nam sinh        sdt
     * nghe nghiep      noi o hien nay
     * suc khoe
     * hoan canh kinh te gia dinh cha me vo     kha gia     du song     kho khan
     * cha me vo sinh duoc  con(    trai    gai) vo dong chi la con thu 
     * III Qua trinh cong tac
     * IV Tinh hinh tu tuong
     * 
     * XAC NHAN CUA BCH TIEU DOAN              XAC NHAN CUA BCH DAI DOI 
     *       CHINH TRI VIEN                          CHINH TRI VIEN
     *                                               
     *                                               
     *  Thieu ta Nguyen Quoc Bao                Dai uy Le Trong Tinh
     */
}
