# Kondongpu

built with
ASP.NET Framework 4.5 ขึ้นไป 
MS SQL Server

--- 

## Technology

* ASP.NET Framework 4.5 ขึ้นไป 
* Vb.Net 
* MS SQL Server
* Visual Studio 2022

## ขั้นตอนการรันใน visual studio

    ใน solution มีหลาย Project เลือกที่ต้องการรัน 


## มาตรฐานการร่วมพัฒนา (โปรดปฏิบัติตามอย่างเคร่งครัด)

    1. Interface ให้ขึ้นต้นชื่อไฟล์และชื่อด้วย I ตัวไอพิมพ์ใหญ่ เช่น IKondongpuDatabaseContext
    2. ให้ตั้งชื่อให้สื่อความหมายและอธิบายตัวเองได้ดี ไม่ว่าจะเป็นชื่อคลาส, ตัวแปร และเมธอด หากไม่แน่ใจให้ปรึกษาทีม
    3. ให้ใช้ PascalCase ในการตั้งชื่อคลาสและเมธอด เช่นคลาส KondongpuDatabaseContext หรือ เมธอด SaveChangesAsync()
    4. ให้ใช้ PascalCase ในการตั้งชื่อค่าคงที่ Constant ทั้งที่เป็น local constants และ Fields เช่น ConnectionString = "Database"
    5. ให้ใช้ camelCase ในการตั้งชื่อ method arguments, local variables, และ private fields เช่นใน Constructor KondongpuDatabase(string connectionString)
    6. ถ้าเป็น private instance ให้ใช้ _ underscore นำหน้า เช่น _context
    7. พารามิเตอร์ในฟังก์ชั่นหรือเมธอด หากมีไม่เกิน 3 ตัว ให้เขียนเรียงต่อกัน แต่ถ้าหากเกิน 3 ตัว ให้ขึ้นบรรทัดใหม่ทุกตัว

## ขั้นตอนเข้าร่วมพัฒนา

    1. Clone source code ได้ที่ https://git.suth.go.th/dev/checkup จาก gitlab ผ่าน SourceTree
    2. สร้าง branch ใหม่ที่แตกออกจาก branch หลักเช่น main หรือ branch ที่กำหนด โดยใช้ชื่อ branch ที่สื่อความหมายเช่น FeatureNameDevelopement หรือ ภาษาไทยเช่น พัฒนาฟังก์ชันการทำงานของการเรียกคิว
    3. พัฒนาคุณสมบัติส่วนที่เกี่ยวข้อง
    4. commit การเปลี่ยนแปลง โดยที่หากเป็นการเปลี่ยนแปลงที่เกี่ยวข้องกับหลายไฟล์สามารถรวมกลุ่มเป็น commit เดียวกันได้ แต่ควรแยกส่วนให้ย่อยที่สุดหากทำได้
    5. การตั้งข้อความ commit พยายามให้สื่อความหมายถึงสิ่งที่เปลี่ยนแปลงและชัดเจนและเข้าใจง่ายเป็นภาษาไทย หรือภาษาอังกฤษ
    6. push เข้า branch ที่ remote เพื่อเก็บไว้บน git server (เฉพาะ branch ตัวเองเท่านั้น) **ห้าม push เข้า branch อื่นโดยตรงเด็ดขาด

## ก่อนการรวม source code เข้ากับ Branch หลัก

    1. ต้องมีการเขียน Unit Tests สำหรับ Domain Entity ก่อนเสมอ อย่างน้อยต้องมี test cases ที่ครอบคลุม constructor สำหรับ field ที่ required
    2. สำหรับ Command หรือ Query ต้องมีการเขียน Integration Tests อย่างน้อยครอบคลุม test cases สำหรับการทำงานที่สำเร็จ, การทำงานที่ไม่สำเร็จที่เกิดจากข้อมูลไม่มี, การทำงานที่ไม่สำเร็จที่เกิดจากการส่งข้อมูลมาไม่ตรงกับ Validation Rules หรือคุณสมบัติที่ค่อนข้างวิกฤตและสำคัญ
    3. การไม่เขียน tests สำหรับทดสอบ code ที่ตัวเองเขียนขึ้น เป็นการทำงานที่ไม่มีคุณภาพมีผลกระทบกับผู้ใช้งานอย่างสูง และสร้างความวิตกกังวลให้กับทีมเป็นอย่างมากในตอน Production
    4. การเขียน tests แม้จะไม่ได้การรันตีว่า Production จะไม่มี BUG 100% แต่ก็สามารถป้องกัน error บางอย่างที่ไม่ควรเกิดขึ้นและสามารถตรวจพบในระหว่างพัฒนาได้เลย ดีกว่าไปเจอที่ Production 

## การรวม source code (Pull Request)

    1. commit สิ่งที่เปลี่ยนแปลง และที่ต้องการ stage ให้เรียบร้อย ทั้งที่ modified และ unstaged
    2. หากต้องการรวม source code ที่อยู่ใน bracnh ของตนเองเข้ากับ branch หลักที่แตกออกมา ให้สลับ branch ไปยัง branch ที่ต้องการรวมแล้ว fetch->pull ลงมาหากมี update ก่อนเสมอ
    3. สลับ branch กลับไปยัง branch ตัวเอง แล้วให้คลิกขวาที่ branch ที่ต้องการรวมแล้วเลือก rebase
    4. หากมี conflict ให้ resolve conflict โดยใช้ DiffMerge และปรึกษาทีม
    5. เมื่อ resolve conflict เสร็จแล้วหรือไม่มี conflict ให้ Push ขึ้น branch ตัวเอง
    6. ไปสร้าง Pull Request ใน https://git.suth.go.th/dev/checkup
    7. เลือก branch ต้นทาง และ ปลายทางให้ถูกต้อง
    8. กำหนดผู้รับผิดชอบ (ไม่ต้องกำหนดหากเป็น branch ตัวเอง) และใส่ข้อความอธิบายเกี่ยวกับการขอรวมในครั้งนี้ให้สั้นกระชับเข้าใจง่าย
    9. บันทึกการขอ Merge หาเป็น branch ตัวเอง ก็ merge เองได้เลย
    10. ผู้ที่ถูก assign หรือ branch ปลายทางจะดำเนินการ pull code ลงมา review และ test ก่อนที่จะ accept merge request หรือ reject merge request
    11. หากรวมแล้วเป็นอันเสร็จสิ้น
    12. pull version ใหม่ลงมา แล้วแตก branch ใหม่เพื่อพัฒนาต่อจากนั้น
    13. ทำตามขั้นตอนแรกวนไป
